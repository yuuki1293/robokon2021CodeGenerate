module public ロボコン2021コード自動生成.Csv

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Text
open System.Windows.Forms
open ロボコン2021コード自動生成.Monad

let ListCopy (list: List<List<int>>) =
    list
        .Select(fun i -> i.Select(fun i -> i).ToList())
        .ToList()


let csvReadLine (stream: OpenFileDialog) =
    try
        File
            .ReadLines(stream.FileName, Encoding.GetEncoding("utf-8"))
            .Select(fun s ->
                s
                    .Replace("{", "")
                    .Replace("}", "")
                    .Split(",")
                    .Where(fun s ->
                        match Int32.TryParse s with
                        | (true, _) -> true
                        | _ -> false)
                    .Select(fun s -> Int32.Parse s))
       :> obj |> Right
    with
    | x -> Left (x.ToString())

let csvToList =
    let dialog = new OpenFileDialog()
    dialog.Title <- "CSVファイルを選択してね"
    dialog.Filter <- "CSVファイル(*.csv)|*.csv"

    if dialog.ShowDialog().Equals(DialogResult.OK) then
        csvReadLine dialog
    else
        Left "ダイアログが正常に閉じられなかったよ"

let rebirths (values: IEnumerable<IEnumerable<int>>) =
    Enumerable
        .Range(0, values.Max(fun c -> c.Count()))
        .Select(fun i ->
            values
                .Select(fun c ->
                    if i < c.Count() then
                        c.ElementAt(i)
                    else
                        0)
                .ToList())
        .ToList()

let (==) (left: List<'T>) (right: List<'T>) =
    [ 0 .. left.Count - 1 ]
        .Any(fun i -> not (left.[i].Equals right.[i]))
    |> not

let listRemove (list: List<List<int>>) =
    let target = List<int>(list.[0])

    for i in [ for i in 0 .. list.Count - 1 -> list.Count - i - 1 ] do
        if list.[i] == target then
            list.RemoveAt(i)

    target

let rec setl (list: List<List<int>>) =
    let aList = List<List<int>> []

    while not (list.Count = 0) do
        aList.Add(listRemove list)

    aList

let listSearch (list: List<List<int>>) (value: List<int>) =
    list.Select(fun i index -> if i == value then Some index else None)
        .Where(fun i ->
            match i with
            | Some (_) -> true
            | _ -> false)
        .First()
        .Value
