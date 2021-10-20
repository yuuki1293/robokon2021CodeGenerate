module Test

open System
open System.IO
open System.Windows.Forms
open NUnit.Framework

[<SetUp>]
let Setup () =
    ()

[<Test>]
[<STAThreadAttribute>]
let Test1 () =
    let dialog= new OpenFileDialog()
    if dialog.ShowDialog().Equals(DialogResult.OK) then
        let reader = new StreamReader(dialog.OpenFile())
        printfn $"%A{reader.ReadLine()}"
        Assert.Pass()