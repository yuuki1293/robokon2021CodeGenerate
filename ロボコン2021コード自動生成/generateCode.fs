module ロボコン2021コード自動生成.generateCode

open System
open System.Linq
open System.Collections.Generic
open ロボコン2021コード自動生成.Csv
let listToStringFunction (list:List<int>)=
    $"""({String.Join(",",list.Select(fun i->i.ToString()))})"""
let generateFunction (list:List<List<int>>) funName=
    let functionArray= list.Select(fun i index-> $"void autoFunc{index}()" + "{" + $"{funName}{listToStringFunction i};" + "}").ToArray()
    String.Join(Environment.NewLine ,functionArray)

let generateArray (rebirth_list:List<List<int>>)(set_list:List<List<int>>)=
    let define = "void (*funcs[])(void)={"
    let inner = String.Join(",",rebirth_list.Select(fun i -> (listSearch set_list i).ToString()|> (+) "autoFunc").ToArray())
    let fin = "};"
    define + inner + fin 