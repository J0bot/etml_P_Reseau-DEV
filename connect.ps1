<#
.NAME
    Untitled
#>

Add-Type -AssemblyName System.Windows.Forms

#region begin GUI{


$HelloWorldForm                  = New-Object system.Windows.Forms.Form
$HelloWorldForm.ClientSize       = '433,145'
$HelloWorldForm.text             = "Form"
$HelloWorldForm.TopMost          = $false

$HelloBtn                        = New-Object system.Windows.Forms.Button
$HelloBtn.text                   = "Connect"
$HelloBtn.width                  = 356
$HelloBtn.height                 = 30
$HelloBtn.Anchor                 = 'top,right,bottom,left'
$HelloBtn.location               = New-Object System.Drawing.Point(28,74)
$HelloBtn.Font                   = 'Microsoft Sans Serif,10'
$HelloBtn.BackColor              = '200,250,30'

$GrettingLabel                   = New-Object system.Windows.Forms.Label
$GrettingLabel.AutoSize          = $true
$GrettingLabel.width             = 25
$GrettingLabel.height            = 10
$GrettingLabel.location          = New-Object System.Drawing.Point(34,24)
$GrettingLabel.Font              = 'Microsoft Sans Serif,10'

$HelloWorldForm.controls.AddRange(@($HelloBtn,$GrettingLabel))


#region gui events {
$HelloBtn.Add_Click({ SayHello })
#endregion events }

#endregion GUI }

#METTRE VOTRE ADRESSE IP ICI
Function SayHello {
    $GrettingLabel.text="Connecting..."
    mstsc /prompt /v:172.20.120.31
}

#Write your logic code here

[void]$HelloWorldForm.ShowDialog()



