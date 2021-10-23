// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
module ロボコン2021コード自動生成.Program

open System
open System.Collections
open System.Collections.Generic
open System.Windows.Forms
open ロボコン2021コード自動生成.Monad
open ロボコン2021コード自動生成.Csv
open ロボコン2021コード自動生成.generateCode
open ロボコン2021コード自動生成.CLanguages
open ロボコン2021コード自動生成.config

[<STAThread>]
[<EntryPoint>]
let main argv =
    let objectFunc = 
        either {
            let! lists = csvToList
            let rebirth_list = rebirths (unbox<IEnumerable<IEnumerable<int>>> lists)
            let set_list = setl (ListCopy rebirth_list)
            let codeBlockStruct = generateStruct set_list
            let! codeBlockFunction = generateFunction funName set_list
            let! codeBlockArray = generateArray rebirth_list set_list

            let completeProgram =
                    "\n"
                    + codeBlockStruct
                    + "\n\n"
                    + unbox codeBlockFunction
                    + "\n\n"
                    + unbox codeBlockArray
                    + "\n"

            return WriteCppFile completeProgram :> obj
        }
    let Clip =
        match objectFunc with
        | Right _ -> MessageBox.Show("完了したよ！", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        | Left x -> MessageBox.Show($"例外\n{x}\nが発生したよ><", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)

    0
