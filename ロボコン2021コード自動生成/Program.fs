// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
module ロボコン2021コード自動生成.Program

open System
open System.Windows.Forms
open ロボコン2021コード自動生成.Monad
open ロボコン2021コード自動生成.Csv
open ロボコン2021コード自動生成.generateCode
open ロボコン2021コード自動生成.CLanguages
open ロボコン2021コード自動生成.config

[<STAThread>]
[<EntryPoint>]
let main _ =
    let objectFunc =
        either {
            let! lists = csvToList
            let rebirth_list = rebirths (unbox lists)
            let set_list = setl (ListCopy rebirth_list)
            let codeBlockStruct = generateStruct set_list
            let! codeBlockFunction = generateFunction funName set_list
            let! codeBlockArray = generateArray rebirth_list set_list

            let completeProgram = $"\n{codeBlockStruct}\n\n{codeBlockFunction}\n\n{codeBlockArray}\n"

            return WriteCppFile completeProgram :> obj
        }

    let MessageShow text title icon =
        MessageBox.Show(
            text,
            title,
            MessageBoxButtons.OK,
            icon,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly
        )

    let Clip =
        match objectFunc with
        | Right _ -> MessageShow "完了したよ！" "Success" MessageBoxIcon.Information
        | Left x -> MessageShow $"例外\n{x}\nが発生したよ><" "エラー" MessageBoxIcon.Error

    0
