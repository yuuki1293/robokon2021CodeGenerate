module ロボコン2021コード自動生成.generateCode

open System
open System.Linq
open System.Collections.Generic
let listToString (list:List<int>)=
    $"""({String.Join(", ",list.Select(fun i->i.ToString()))})"""
let generateFunction (list:List<List<int>>) funName=
    let functionArray= list.Select(fun i index-> $"void autoFunc{index}()" + "{" + $"{funName}{listToString i};" + "}").ToArray()
    String.Join(Environment.NewLine ,functionArray)