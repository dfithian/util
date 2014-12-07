gmcs "/out:bin/Fithian.dll" -t:library -r:Newtonsoft.Json.dll -lib:lib/util/Json60r6/Bin/Net40/ /nologo /warn:4 /debug:full /optimize- /codepage:utf8 "/define:TRACE;DEBUG" -recurse:'src/*.cs'

if [[ $1 == "test" ]]; then
    gmcs "/out:test/bin/Test.exe" -r:Fithian.dll,Newtonsoft.Json.dll -lib:bin/,lib/util/Json60r6/Bin/Net40/ /nologo /warn:4 /debug:full /optimize- /codepage:utf8 "/define:TRACE;DEBUG" -recurse:'test/src/*.cs'
    cp ./bin/Fithian.dll ./test/bin/Fithian.dll
    cp ./lib/util/Json60r6/Bin/Net40/Newtonsoft.Json.dll ./test/bin/Newtonsoft.Json.dll
    mono --runtime=v4.0 ./test/bin/Test.exe
fi
