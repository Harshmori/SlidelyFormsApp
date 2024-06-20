Imports System.Net
Imports Newtonsoft.Json

Public Class ViewSubmissionsForm
    Private submissions As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))()
    Private currentIndex As Integer = 0

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal searchResults As List(Of Dictionary(Of String, String)))
        InitializeComponent()
        Me.submissions = searchResults
        Me.currentIndex = 0
        DisplaySubmission()
        UpdateButtonStates()
    End Sub

    Private Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If submissions.Count = 0 Then
            FetchSubmissions()
        End If
        DisplaySubmission()
        UpdateButtonStates()
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission()
        End If
        UpdateButtonStates()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission()
        End If
        UpdateButtonStates()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If MessageBox.Show("Are you sure you want to delete this submission?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                ' Delete the submission from the backend
                Dim client As New WebClient()
                Dim response As String = client.UploadString("http://localhost:3000/delete/" & currentIndex, "DELETE", "")

                ' Log the response to ensure it's correct
                Console.WriteLine("Delete Response: " & response)

                ' Fetch updated submissions after deletion
                FetchSubmissions()

                ' Display the next submission or clear the fields if no submissions left
                If submissions.Count > 0 Then
                    If currentIndex >= submissions.Count Then
                        currentIndex = submissions.Count - 1
                    End If
                    DisplaySubmission()
                Else
                    ClearFields()
                End If

                ' Update button states
                UpdateButtonStates()
            Catch ex As Exception
                ' Handle and log exceptions
                Console.WriteLine("Error deleting submission: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        ' Enable editing of textboxes
        txtName.ReadOnly = False
        txtEmail.ReadOnly = False
        txtPhone.ReadOnly = False
        txtGithubLink.ReadOnly = False
        txtStopwatchTime.ReadOnly = False

        ' Show submit and cancel buttons
        btnSubmit.Visible = True
        btnCancel.Visible = True

        ' Hide edit button
        btnEdit.Visible = False
        btnDelete.Visible = False

        ' Disable next and previous buttons while editing
        btnNext.Enabled = False
        btnPrevious.Enabled = False
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ' Disable editing of textboxes
        txtName.ReadOnly = True
        txtEmail.ReadOnly = True
        txtPhone.ReadOnly = True
        txtGithubLink.ReadOnly = True
        txtStopwatchTime.ReadOnly = True

        ' Hide submit and cancel buttons
        btnSubmit.Visible = False
        btnCancel.Visible = False

        ' Show edit button
        btnEdit.Visible = True
        btnDelete.Visible = True

        ' Re-enable next and previous buttons
        UpdateButtonStates()
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' Update the submission in the submissions list
        Dim submission = submissions(currentIndex)
        submission("name") = txtName.Text
        submission("email") = txtEmail.Text
        submission("phone") = txtPhone.Text
        submission("github_link") = txtGithubLink.Text
        submission("stopwatch_time") = txtStopwatchTime.Text

        ' Send a PUT request to update the submission in the backend
        Try
            Dim client As New WebClient()
            Dim json As String = JsonConvert.SerializeObject(submission)
            client.Headers(HttpRequestHeader.ContentType) = "application/json"
            Dim response As String = client.UploadString("http://localhost:3000/edit/" & currentIndex, "PUT", json)

            ' Log the response to ensure it's correct
            Console.WriteLine("Response: " & response)

            ' Refresh the displayed submission
            FetchSubmissions()
            DisplaySubmission()
        Catch ex As Exception
            ' Handle and log exceptions
            Console.WriteLine("Error updating submission: " & ex.Message)
        End Try

        ' Reset the form state
        txtName.ReadOnly = True
        txtEmail.ReadOnly = True
        txtPhone.ReadOnly = True
        txtGithubLink.ReadOnly = True
        txtStopwatchTime.ReadOnly = True

        btnSubmit.Visible = False
        btnCancel.Visible = False
        btnEdit.Visible = True
        btnDelete.Visible = True
        btnNext.Enabled = True
        btnPrevious.Enabled = True
    End Sub

    Private Sub UpdateButtonStates()
        ' Disable previous button if at first submission
        btnPrevious.Enabled = currentIndex > 0
        If btnPrevious.Enabled Then
            btnPrevious.BackColor = Color.Gold ' Enabled background color
            btnPrevious.ForeColor = Color.Black ' Enabled text color
        Else
            btnPrevious.BackColor = Color.LightGray ' Disabled background color
            btnPrevious.ForeColor = Color.DarkGray ' Disabled text color
        End If

        ' Disable next button if at last submission
        btnNext.Enabled = currentIndex < submissions.Count - 1
        If btnNext.Enabled Then
            btnNext.BackColor = Color.Gold ' Enabled background color
            btnNext.ForeColor = Color.Black ' Enabled text color
        Else
            btnNext.BackColor = Color.LightGray ' Disabled background color
            btnNext.ForeColor = Color.DarkGray ' Disabled text color
        End If
    End Sub

    Private Sub DisplaySubmission()
        If submissions.Count > 0 Then
            Dim submission As Dictionary(Of String, String) = submissions(currentIndex)
            txtName.Text = submission("name")
            txtEmail.Text = submission("email")
            txtPhone.Text = submission("phone")
            txtGithubLink.Text = submission("github_link")
            txtStopwatchTime.Text = submission("stopwatch_time")
        End If
    End Sub

    Private Sub FetchSubmissions()
        Try
            ' Fetch all submissions from the backend
            Dim client As New WebClient()
            Dim response As String = client.DownloadString("http://localhost:3000/read") ' Fetch all submissions

            ' Log the response to ensure it's correct
            Console.WriteLine("Response: " & response)

            ' Deserialize the response to the expected type
            submissions = JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, String)))(response)
        Catch ex As Exception
            ' Handle and log exceptions
            Console.WriteLine("Error fetching submissions: " & ex.Message)
        End Try
    End Sub

    Private Sub ClearFields()
        ' Clear all textboxes
        txtName.Clear()
        txtEmail.Clear()
        txtPhone.Clear()
        txtGithubLink.Clear()
        txtStopwatchTime.Clear()
    End Sub

    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            btnPrevious.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnNext.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.M Then
            btnEdit.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.D Then
            btnDelete.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.C Then
            btnCancel.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit.PerformClick()
        End If
    End Sub
End Class
