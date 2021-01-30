module Fantomas.Tests.RemoveUnnecessaryParenthesesTests

open NUnit.Framework
open FsUnit
open Fantomas.Tests.TestHelper

[<Test>]
let ``parentheses around single identifiers in match patterns are unnecessary, 684`` () =
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
let ``parentheses around non-single identifiers in match patterns are kept, I`` () =
    formatSourceString
        false
        """
match foo with
| Bar (Baz baz) -> ()
| _ ->
    failwith "xxx"
"""
        config
    |> prepend newline
    |> should
        equal
        """
match foo with
| Bar (Baz baz) -> ()
| _ -> failwith "xxx"
"""


[<Test>]
let ``parentheses around non-single identifiers in match patterns are kept, II`` () =
    formatSourceString
        false
        """
match foo with
| Bar ({ Baz = baz } as x) -> ()
| _ ->
    failwith "xxx"
"""
        config
    |> prepend newline
    |> should
        equal
        """
match foo with
| Bar ({ Baz = baz } as x) -> ()
| _ -> failwith "xxx"
"""
