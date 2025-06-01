namespace KwestKarz.TextProcessor

open System
open LagDaemon.Templates.TemplateParser

module Template =

    type EmailTemplate(subject: string, body: string) =
        let mutable _subject = subject
        let mutable _body = body

        let replacers : (string * Replacer) list = [
            ("date", Replacer (fun _ prms -> 
                match prms with
                | [] -> DateTime.Now.Date.ToString()
                | h::_ -> DateTime.Now.Date.ToString(h)
            ))
            ("time", Replacer (fun _ prms -> 
                match prms with
                | [] -> DateTime.Now.TimeOfDay.ToString()
                | h::_ -> DateTime.Now.TimeOfDay.ToString(h)
            ))
            ("datetime", Replacer (fun _ prms -> 
                match prms with
                | [] -> DateTime.Now.ToString()
                | h::_ -> DateTime.Now.ToString(h)
            ))
            ("user", Replacer (fun _ _ -> "Dave"))
            ("system", Replacer (fun _ _ -> "LagDaemon MUD"))
        ]

        let map = addList (ReplacementMap.create()) replacers

        member this.Subject 
            with get() = _subject 
            and set(value) = _subject <- value

        member this.Body 
            with get() = _body 
            and set(value) = _body <- value

        member this.Process() =
            let applyTemplate text =
                match processString text map with
                | Ok result -> result
                | Error msg -> raise (Exception($"Template processing failed: {msg}"))

            _subject <- applyTemplate _subject
            _body <- applyTemplate _body
            this
