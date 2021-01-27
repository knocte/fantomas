module Fantomas.Tests.RemoveUnnecessaryParenthesesTests

open NUnit.Framework
open FsUnit
open Fantomas.Tests.TestHelper

[<Test>]
let ``parentheses around single identifiers in match patterns are unnecessary`` () =
    formatSourceString
        false
        """
match foo with
| Bar (baz) -> ()
| _ ->
    failwith "xxx"
"""
        config
    |> prepend newline
    |> should
        equal
        """
match foo with
| Bar baz -> ()
| _ -> failwith "xxx"
"""

[<Test>]
let ``comments around single identifiers with unnecessary parenthesis should be preserved `` () =
    formatSourceString
        false
        """
match foo with
| Bar (baz) (* comment *) -> ()
| _ ->
    failwith "xxx"
"""
        config
    |> prepend newline
    |> should
        equal
        """
match foo with
| Bar baz (* comment *) -> ()
| _ -> failwith "xxx"
"""
