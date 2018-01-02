$path=split-path $MyInvocation.MyCommand.path 
$spath= "$path\msi\MicrosoftSpeechPlatformSDK.msi" 
$spath1= "$path\msi\MSSpeech_SR_de-DE_TELE.msi" 
$spath2= "$path\msi\SpeechPlatformRuntime.msi" 
$spath3= "$path\msi\MSSpeech_TTS_de-DE_Hedda.msi" 
If($global:availability -eq $null) 
{ 
    "1. Local Administrator software is not installed in this computer" 
    If(Test-Path $spath) 
        { 
            "2. MSI file is accessible from the directory " 
            $status=Start-Process -FilePath msiexec.exe -ArgumentList '/i',$spath,'/q' -Wait -PassThru -Verb "RunAs" 
            $status=Start-Process -FilePath msiexec.exe -ArgumentList '/i',$spath1,'/q' -Wait -PassThru -Verb "RunAs" 
            $status=Start-Process -FilePath msiexec.exe -ArgumentList '/i',$spath2,'/q' -Wait -PassThru -Verb "RunAs" 
            $status=Start-Process -FilePath msiexec.exe -ArgumentList '/i',$spath3,'/q' -Wait -PassThru -Verb "RunAs" 
	    "Done"
            If($?) 
        { 
            checksoftware 
                    "3.  $($Global:availability.DisplayName)--$($Global:availability.DisplayVersion) has been installed" 
        } 
        else{"3. Unable to install the software"} 
        }    
    Else 
        { 
               "2. Unable to access the MSI file form directory" 
        } 
} 
Else 
{ 
    "1. Local Administrator software is already existing" 
}*