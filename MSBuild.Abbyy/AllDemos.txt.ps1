#All Demos
# Init
    cd p:\msbuild\Code


# Syntax.proj
    msbuild ./Syntax.proj


# PropEval.proj
    msbuild ./Syntax.proj
    msbuild ./Syntax.proj /p:"AbbyyProp=CommandLine-Abbyy"
    gci env:
    $env:AbbyyProp="EnvironmentValue-Abbyy"
    msbuild ./Syntax.proj
    ri env:AbbyyProp


# ItemsMeta.proj
    msbuild ./ItemsMeta.proj /p:"DemoFile=1"
    msbuild ./ItemsMeta.proj /p:"DirContent=1"
    msbuild ./ItemsMeta.proj /p:"StringList=1"


# InlineTask.proj
    #показать и рассказать про InlineTask.msbuild
    msbuild ./InlineTask.proj
    

# Targets.proj
    msbuild ./Targets.proj


# BuildHooks.proj
    msbuild .\BigProject\Source\Autofac\Autofac.csproj /t:"clean;build" /p:"CustomAfterMicrosoftCommonTargets=p:\MSBuild\Code\BuildHooks.proj" /v:detailed > ./_BuildLog.AutoFac.log
    # показать содержимое BuildLog.AutoFac.log 
    # найти выполненные Targets по ====

# Visual Studio Debugging
    msbuild .\BigProject\Source\Autofac\Autofac.csproj /t:"clean;build" /debug
    
    #Ctrl+D,I
    EvaluateCondition("'$(SchemaVersion)' != '3.0'")
    Project.SetProperty("Platform", "AnyCPU")
    EvaluateExpression("@(Compile)").Split(';')


