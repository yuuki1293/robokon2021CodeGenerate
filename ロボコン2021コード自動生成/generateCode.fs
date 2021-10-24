module ロボコン2021コード自動生成.generateCode

open System
open System.Linq
open System.Collections.Generic
open ロボコン2021コード自動生成.Monad
open ロボコン2021コード自動生成.Csv

let listToStringFunction (list: List<int>) =
    $"""{String.Join(",", list.Select(fun i -> i.ToString()))}"""

let generateStruct (list: List<List<int>>) =
    let functionArray =
        list
            .Select(fun i index -> $"int data{index}[24] = {{{listToStringFunction i}}};")
            .ToArray()

    String.Join(Environment.NewLine, functionArray)

let generateFunction funName (count: List<List<int>>) =
    String.Join(
        "\n",
        [ 0 .. count.Count - 1 ]
            .Select(fun i -> $"void autoFunc{i}(){{{funName}(data{i});}}")
    )
    :> obj
    |> Right


let joinList (list: List<List<int>>) (set_list: List<List<int>>) num =
    let define = $"void (*funcs{num}[])(void)={{"

    let inner =
        String.Join(
            ",",
            list
                .Select(fun i ->
                    (listSearch set_list i).ToString()
                    |> (+) "autoFunc")
                .ToArray()
        )

    let fin = "};"
    define + inner + fin

let generateArray (rebirth_list: List<List<int>>) (set_list: List<List<int>>) =
    if rebirth_list.Count % 30 <> 0 then
        Left "要素数が30の倍数じゃないよ！"
    else
        let funcs =
            [ 1 .. (rebirth_list.Count / 30) ]
                .Select(fun i -> rebirth_list.GetRange(30 * (i - 1), 30))
                .Select(fun i index -> joinList i set_list index)

        String.Join("\n", funcs) :> obj |> Right
