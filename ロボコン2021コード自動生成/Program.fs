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
    let codeBlockFunction = generateFunction set_list funName
    let codeBlockArray = generateArray rebirth_list set_list
    let completeProgram ="\n" + codeBlockFunction + "\n\n" + codeBlockArray + "\n"
    WriteCppFile completeProgram
    Clipboard.SetText completeProgram
    0
