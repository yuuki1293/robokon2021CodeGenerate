module ロボコン2021コード自動生成.CLanguages

open System
open System.IO
open System.Windows.Forms
open ロボコン2021コード自動生成.config
open ロボコン2021コード自動生成.Monad

let OpenCppFile =
    either {
        let dialog = new OpenFileDialog()
        dialog.Title <- "cppファイルを選択してね"
        dialog.Filter <- "cファイル(*.c;*.cpp)|*.c;*.cpp"

        let! path =
            if dialog.ShowDialog().Equals(DialogResult.OK) then
                Right(dialog.FileName :> obj)
            else
                Left "ダイアログが正常に閉じられなかったよ！"

        return path
    }

let SearchFlag (text: string) =
    either {
        let! beginInsert =
            match text.IndexOf comment with
            | -1 -> Left $"{comment} が見つからなかったよ！"
            | x -> Right(x + comment.Length :> obj)

        let! endInsert =
            match text.LastIndexOf comment with
            | x when x.Equals beginInsert -> Left $"{comment} が1つしか見つからなかったよ！"
            | x -> Right(x - unbox<int> beginInsert :> obj)

        return
            [ unbox<int> beginInsert
              unbox<int> endInsert ]
            :> obj
    }

let WriteCppFile (text: string) =
    either {
        let! filePath = OpenCppFile

        let! readCode =
            try
                Right(File.ReadAllText(unbox filePath) :> obj)
            with
            | x -> Left(x.ToString())

        let code = unbox<string> readCode
        let! searchIndex = SearchFlag code

        let searchIndexList: List<int> = unbox searchIndex
        let beginInsert = searchIndexList.[0]
        let count = searchIndexList.[1]

        let removedCode = code.Remove(beginInsert, count)

        let insertedCode = removedCode.Insert(beginInsert, text)

        return File.WriteAllText(unbox filePath, insertedCode) :> obj
    }
