Imports Newtonsoft.Json
Imports System.Net

Public Class SearchSubmissionsForm

    Private Sub SearchSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbField.Items.Add("Name")
        cmbField.Items.Add("Email")
        cmbField.Items.Add("Phone")
        cmbField.Items.Add("Github Link")
        cmbField.SelectedIndex = 0
    End Sub

    Private Sub btnSubmitSearch_Click(sender As Object, e As EventArgs) Handles btnSubmitSearch.Click
        Dim field As String = cmbField.SelectedItem.ToString().ToLower().Replace(" ", "_")
        Dim value As String = txtSearchValue.Text

        If String.IsNullOrEmpty(value) Then
            MessageBox.Show("Please enter a search value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' URL encode the search value
        Dim encodedValue As String = Uri.EscapeDataString(value)

        Try
            Dim client As New WebClient()
            Dim response As String = client.DownloadString($"http://localhost:3000/search/{field}/{encodedValue}")

            Dim results As List(Of Dictionary(Of String, String)) = JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, String)))(response)

            If results.Count > 0 Then
                Dim viewSubmissionsForm As New ViewSubmissionsForm(results)
                viewSubmissionsForm.Show()
            Else
                MessageBox.Show("No results found.", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error fetching search results: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmitSearch.PerformClick()
        End If
    End Sub
End Class
