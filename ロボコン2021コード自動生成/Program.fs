// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
module ロボコン2021コード自動生成.Program

open System
open System.Windows.Forms
open ロボコン2021コード自動生成.Csv
open ロボコン2021コード自動生成.generateCode
open ロボコン2021コード自動生成.CLanguages

let funName = "ashi"

[<STAThread>]
[<EntryPoint>]
let main argv =
    let lists = csvToList ()
    let rebirth_list = rebirths lists.Value
    let set_list = setl (ListCopy rebirth_list)
    let codeBlockStruct = generateStruct set_list funName
    let codeBlockFunction = generateFunction  funName set_list.Count
    let codeBlockArray = generateArray rebirth_list set_list
    let completeProgram ="\n" + codeBlockStruct + "\n\n" + codeBlockFunction + "\n\n" + codeBlockArray + "\n"
    WriteCppFile completeProgram
    Clipboard.SetText completeProgram
    0
