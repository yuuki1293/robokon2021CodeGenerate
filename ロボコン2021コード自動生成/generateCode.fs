module ロボコン2021コード自動生成.generateCode

open System
open System.Linq
open System.Collections.Generic
open ロボコン2021コード自動生成
open ロボコン2021コード自動生成.Csv

let listToStringFunction (list: List<int>) =
    $"""{String.Join(",", list.Select(fun i -> i.ToString()))}"""

let generateStruct (list: Option<List<List<int>>>) funName =
    if list.IsNone then
        None
    else
        let functionArray =
            list
                .Value
                .Select(fun i index -> $"int data{index}[24] = {{{listToStringFunction i}}};")
                .ToArray()

        String.Join(Environment.NewLine, functionArray) |> Some

let generateFunction funName (count:Option<List<List<int>>>) =
    if count.IsNone then None else
    String.Join(
        "\n",
        [ 0 .. count.Value.Count - 1 ]
            .Select(fun i -> $"void autoFunc{i}(){{{funName}(data{i});}}")
    ) |> Some

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

let generateArray (rebirth_list: Option<List<List<int>>>) (set_list: Option<List<List<int>>>) =
    if rebirth_list.IsNone || set_list.IsNone then None else
    let funcs =
        [ 1 .. (rebirth_list.Value.Count / 30) ]
            .Select(fun i -> rebirth_list.Value.GetRange(30 * (i - 1), 30))
            .Select(fun i index -> joinlist i set_list.Value index)

    String.Join("\n", funcs) |> Some
