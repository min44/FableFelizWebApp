module App

open Feliz
open Elmish
open Elmish.React

type Person =
    { Name: string
      Age: int }

let persons =
    [ { Name = "John";  Age = 26;  }
      { Name = "Bobby"; Age = 16;  }
      { Name = "Ann";   Age = 32;  } ]

type Model =
    { FilterBox: string
      Persons: Person list
      PersonsFiltered: Person list }

type Msg = SetFilterBox of string

let init () =
  { FilterBox = ""
    Persons = persons
    PersonsFiltered = persons }, Cmd.none

let update msg model =
    match msg with
    | SetFilterBox text ->
        let filter p = p.Name.Contains(text) || p.Age.ToString().Contains(text)
        let persons = model.Persons
        let personsFiltered = List.filter filter persons
        { model with
            FilterBox = text
            PersonsFiltered = personsFiltered }, Cmd.none

let inputField model dispatch =
    Html.div [
        prop.classes [ "field"; "has-addons" ]
        prop.children [
        Html.div [
        prop.classes [ "control"; "is-expanded" ]
        prop.children [
        Html.input [
        prop.classes [ "input"; "is-medium" ]
        prop.valueOrDefault model.FilterBox
        prop.onTextChange (SetFilterBox >> dispatch) ] ] ] ] ]

let filteredList model =
    Html.ul [
    prop.children [
    for item in model.PersonsFiltered ->
    Html.li [
    prop.classes [ "box"; "subtitle" ]
    prop.children [
    Html.div [ prop.text item.Name ]
    Html.div [ prop.text item.Age ] ] ] ] ]

let appTitle =
    Html.p [
    prop.className "title"
    prop.text "Filter great list" ]

let render state dispatch =
    Html.div [
    prop.style [ style.padding 20 ]
    prop.children [
    appTitle
    inputField state dispatch
    filteredList state ] ]

Program.mkProgram init update render
|> Program.withConsoleTrace
|> Program.withReactSynchronous "app"
|> Program.run
