// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
module ロボコン2021コード自動生成.Program

open System
open ロボコン2021コード自動生成.Csv
open ロボコン2021コード自動生成.generateCode

let funName = "hoge"

[<STAThread>]
[<EntryPoint>]
let main argv =
    let lists = csvToList ()
    let rebirth_list = rebirths lists.Value
    let set_list = setl rebirth_list
    let s = generateFunction set_list funName
    0
