Imports Newtonsoft.Json
Imports System.Net
Imports System.Text.RegularExpressions

Public Class CreateSubmissionForm
    Private stopwatchRunning As Boolean = False
    Private stopwatchTime As TimeSpan = TimeSpan.Zero

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        If stopwatchRunning Then
            timerStopwatch.Stop()
            btnToggleStopwatch.Text = "TOGGLE STOPWATCH (CTRL + T)"
        Else
            timerStopwatch.Start()
            btnToggleStopwatch.Text = "STOP STOPWATCH (CTRL + T)"
        End If
        stopwatchRunning = Not stopwatchRunning
    End Sub

    Private Sub timerStopwatch_Tick(sender As Object, e As EventArgs) Handles timerStopwatch.Tick
        stopwatchTime = stopwatchTime.Add(TimeSpan.FromSeconds(1))
        lblStopwatchTime.Text = stopwatchTime.ToString("hh\:mm\:ss")
    End Sub

    Private Function IsValidEmail(email As String) As Boolean
        Dim emailPattern As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"
        Return Regex.IsMatch(email, emailPattern)
    End Function

    Private Function IsValidGithubLink(githubLink As String) As Boolean
        Dim githubPattern As String = "^https:\/\/github\.com\/[a-zA-Z0-9\-_]+$"
        Return Regex.IsMatch(githubLink, githubPattern)
    End Function

    Private Function IsValidPhoneNumber(phoneNumber As String) As Boolean
        Dim phonePattern As String = "^\d{10}$"
        Return Regex.IsMatch(phoneNumber, phonePattern)
    End Function

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim email As String = txtEmail.Text
        Dim githubLink As String = txtGithubLink.Text
        Dim phoneNumber As String = txtPhone.Text

        If Not IsValidEmail(email) Then
            MessageBox.Show("Please enter a valid email address.")
            Return
        End If

        If Not IsValidGithubLink(githubLink) Then
            MessageBox.Show("Please enter a valid GitHub link.")
            Return
        End If

        If Not IsValidPhoneNumber(phoneNumber) Then
            MessageBox.Show("Please enter a valid 10-digit phone number.")
            Return
        End If

        Dim submission As New Dictionary(Of String, String) From {
            {"name", txtName.Text},
            {"email", email},
            {"phone", phoneNumber},
            {"github_link", githubLink},
            {"stopwatch_time", lblStopwatchTime.Text}
        }

        Dim client As New WebClient()
        client.Headers(HttpRequestHeader.ContentType) = "application/json"
        Dim json As String = JsonConvert.SerializeObject(submission)
        Dim response As String = client.UploadString("http://localhost:3000/submit", "POST", json)
        MessageBox.Show("Submission saved successfully")
    End Sub

    Private Sub CreateSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.T Then
            btnToggleStopwatch.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit.PerformClick()
        End If
    End Sub


End Class
