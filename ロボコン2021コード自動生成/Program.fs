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
    let rebirth_list = rebirths lists
    let set_list = setl (ListCopy rebirth_list)
    let codeBlockStruct = generateStruct set_list funName
    let codeBlockFunction = generateFunction funName set_list
    let codeBlockArray = generateArray rebirth_list set_list

    let completeProgram =
        if codeBlockArray.IsNone
           || codeBlockFunction.IsNone
           || codeBlockStruct.IsNone then
            None
        else
            "\n"
            + codeBlockStruct.Value
            + "\n\n"
            + codeBlockFunction.Value
            + "\n\n"
            + codeBlockArray.Value
            + "\n" |> Some

    WriteCppFile completeProgram |> ignore
    let Clip = if completeProgram.IsNone then fun _ ->
                    MessageBox.Show("例外が発生しました","エラー",MessageBoxButtons.OK,MessageBoxIcon.Error) |> ignore
                    ()
                else
                    fun _ -> Clipboard.SetText completeProgram.Value
    Clip()
    0
