module ロボコン2021コード自動生成.CLanguages

open System
open System.IO
open System.Windows.Forms
open ロボコン2021コード自動生成.config

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

    if beginInsert.IsNone then
        (None, None)
    else
        let endInsert =
            match text.LastIndexOf comment with
            | -1 -> None
            | x -> Some(x - beginInsert.Value)

        (beginInsert, endInsert)

let WriteCppFile (text: Option<string>) =
    if text.IsNone then
        None
    else
        let filePath = OpenCppFile

        if filePath.IsNone then
            None
        else
            let readCode = File.ReadAllText(filePath.Value)
            let (beginInsert, count) = SearchFlag readCode

            if count.IsNone then
                None
            else
                let removedCode =
                    readCode.Remove(beginInsert.Value, count.Value)

                let insertedCode =
                    removedCode.Insert(beginInsert.Value, text.Value)

                File.WriteAllText(filePath.Value, insertedCode)
                |> Some
