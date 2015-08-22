if (Get-ItemProperty -Name "DebuggerEnabled" -path 'HKLM:\SOFTWARE\Microsoft\MSBuild\4.0' -erroraction silentlycontinue) { 
    "Registry key HKLM:\SOFTWARE\Microsoft\MSBuild\4.0\DebuggerEnabled already exists." 
} else { 
    New-ItemProperty "HKLM:\SOFTWARE\Microsoft\MSBuild\4.0" -Name "DebuggerEnabled" -Value 'true' -PropertyType "String" 
} 

if (Get-ItemProperty -Name "DebuggerEnabled" -path 'HKLM:\SOFTWARE\Wow6432Node\Microsoft\MSBuild\4.0' -erroraction silentlycontinue) { 
    "Registry key HKLM:\SOFTWARE\Wow6432Node\Microsoft\MSBuild\4.0\DebuggerEnabled already exists." 
} else { 
    New-ItemProperty "HKLM:\SOFTWARE\Wow6432Node\Microsoft\MSBuild\4.0" -Name "DebuggerEnabled" -Value 'true' -PropertyType "String" 
} 

