module ロボコン2021コード自動生成.generateCode

open System
open System.Linq
open System.Collections.Generic
open ロボコン2021コード自動生成
open ロボコン2021コード自動生成.Csv

let listToStringFunction (list: List<int>) =
    $"""{String.Join(",", list.Select(fun i -> i.ToString()))}"""

let generateStruct (list: List<List<int>>) funName =
    let functionArray =
        list
            .Select(fun i index -> $"int data{index}[24] = {{{listToStringFunction i}}};")
            .ToArray()

    String.Join(Environment.NewLine, functionArray)

let generateFunction funName count =
    String.Join(
        "\n",
        [ 0 .. count - 1 ]
            .Select(fun i -> $"void autoFunc{i}(){{{funName}(data{i});}}")
    )

let (/%) (left: int) (right: int) =
    left / right
    + match left % right with
      | 0 -> 0
      | _ -> 1

let joinlist (list: List<List<int>>) (set_list: List<List<int>>) num =
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
    let funcs =
        [ 1 .. (rebirth_list.Count / 30) ]
            .Select(fun i -> rebirth_list.GetRange(30 * (i - 1), 30))
            .Select(fun i index -> joinlist i set_list index)

    String.Join("\n", funcs)
