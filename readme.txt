///////////// Necessary to have installed //////////////////////////
Project was created in Visual Studio Comunity 2017 Version 15.9.16
Its necessary to have installed Microsoft.AspNetCore 2.1.1

Necassary to install encoding libs to avoid problems with text files 
converting 1251 to utf-8 by commands in command line:

dotnet add package System.Text.Encoding.CodePage
dotnet dev-certs https --trust
////////////////////////////////////////////////////////////////////

Для компиляции проекта необходимо иметь установленным AspNetCore 2.1.1
Для избежания проблем с кодированием текстовых файлов из кодовой 
страницы 1251 в utf-8 нужно установить необходимые кодировщики,
выполнив в консоли команды:

otnet add package System.Text.Encoding.CodePage
dotnet dev-certs https --trust

