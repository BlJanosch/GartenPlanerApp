# Klassendiagramme

## Pflanze

| Properties & Feldvariablen | Konstruktoren | Methoden
|:-------:|:-------:|:-------:|
|+ <get, set> Name:string| + Pflanze() (Default) | + Tränken():void
|+ <get, set> Beschreibung:string| | + Ernten():void
|+ <get, set> ZeitraumVon:Date| | + Draw():void
|+ <get, set> ZeitraumBis:Date|
|+ <get, set> Wasser:double|
|+ <get, set> Wachsdauer:double|
|+ <get, set> Symbol:string|

## PflanzenCollection

|Properties & Feldvariablen| Konstruktoren | Methoden
|:------------------------:|:-------------:|:------:|
|+ <get, set> PflanzenList:Pflanze| + PflanzenCollection() (Default) | + DrawAllPflanzen()
|+ <get, set> Columns:int| | + UpdateList():void
|+ <get, set> Rows:int| 

## Beet

|Properties & Feldvariablen| Konstruktoren | Methoden 
|:------------------------:|:-------------:|:-------|
|+ <get, set> BeetItemList:Pflanze| + Beet() (Default) | + AddPflanze(Pflanze p):void |
||| + RemovePflanze():void
||| + DrawBeet():void
||| + CalculateChemie():void

## BeeteCollection

|Properties & Feldvariablen| Konstruktoren | Methoden 
|:------------------------:|:-------------:|:-------|
|+ <get, set> BeetList:Beet| + BeeteCollection() (Default) | + AddBeet(Beet b):void |
||| + RemoveBeet():void
||| + DrawAllBeete():void

## User

| Properties & Feldvariablen | Konstruktoren | Methoden
|:-:|:-:|:-:|
| + <get, set> UserName:string | + User(name:string, password:string):void | 
| + <get, private set> Password:string | Default() |


