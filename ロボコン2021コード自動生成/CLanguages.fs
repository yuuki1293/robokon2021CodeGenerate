module ロボコン2021コード自動生成.CLanguages

open System
open System.IO
open System.Windows.Forms

let comment = "/* ここに挿入してね */"

let OpenCppFile =
    let dialog = new OpenFileDialog()
    dialog.Title <- "cppファイルを選択してね"
    dialog.Filter <- "cファイル(*.c;*.cpp)|*.c;*.cpp"

    if dialog.ShowDialog().Equals(DialogResult.OK) then
        Some dialog.FileName
    else
        None

let SearchFlag (text: string) =
    let beginInsert =
        match text.IndexOf comment with
        | -1 -> None
        | x -> Some(x + comment.Length)

    let endInsert =
        match text.LastIndexOf comment with
        | -1 -> None
        | x -> Some (x - beginInsert.Value)

    (beginInsert, endInsert)

let WriteCppFile text =
    let filePath = OpenCppFile
    let readCode = File.ReadAllText(filePath.Value)
    let (beginInsert, count) = SearchFlag readCode
    let removedCode =
        readCode.Remove(beginInsert.Value, count.Value)
    let insertedCode = removedCode.Insert(beginInsert.Value,text)
    File.WriteAllText(filePath.Value,insertedCode)
