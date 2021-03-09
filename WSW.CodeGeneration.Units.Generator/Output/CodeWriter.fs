namespace WSW.CodeGeneration.Units.Generator

open Microsoft.CSharp
open System.CodeDom
open System.CodeDom.Compiler
open System.IO
open WSW.CodeGeneration.Units.Generator

type FileContents =
    {
        FileName : string
        FolderName : string
        Code : CodeCompileUnit
    }        

module CodeWriter =
    let private CodeProvider = new CSharpCodeProvider();
    let private CodeOptions = new CodeGeneratorOptions(BracingStyle = "C", VerbatimOrder = true, BlankLinesBetweenMembers = true)
    
    let CreateDirectory (OutputRoot root) folderName =
        let directory = Path.Combine [|
            root
            folderName
        |]
        if Directory.Exists(directory) then 
            new DirectoryInfo(directory)
        else
            Directory.CreateDirectory(directory)
    
    let GeneratedSuffixAndExtension (provider:CodeDomProvider) = ".Generated." + provider.FileExtension
    let FileName fileName provider = fileName + (GeneratedSuffixAndExtension provider)        
    
    let FullFileName (root:OutputRoot) (file:FileContents) (provider:CodeDomProvider) =         
        let directory = file.FolderName |> CreateDirectory root
        Path.Combine [|
            directory.FullName
            (FileName file.FileName provider)
        |]
    
    let WriteToFile (filePath:string) (provider:CodeDomProvider) (options:CodeGeneratorOptions) (unit:CodeCompileUnit) =
        using(new StreamWriter(filePath, false)) (fun writer -> 
            provider.GenerateCodeFromCompileUnit(unit, writer, options)
        )
    
    let AllExistingGeneratedFiles (OutputRoot root) (provider:CodeDomProvider) =
        let pattern = sprintf "*%s" (GeneratedSuffixAndExtension provider)
        Directory.GetFiles(root, pattern, SearchOption.AllDirectories)
    
    let private DeleteAllFiles files : unit =
        for file in files do
            File.Delete(file)
    
    let DeleteAllCode (root:OutputRoot) =
        let provider = CodeProvider
        AllExistingGeneratedFiles root provider |> DeleteAllFiles
        
    let WriteCode (root:OutputRoot) (file:FileContents)  =
        let provider = CodeProvider
        let fullFileName = FullFileName root file provider
        WriteToFile fullFileName provider CodeOptions file.Code
        fullFileName