@echo OFF
echo "Welcome to my cmd file"
if not exist H:\main (
    mkdir H:\main
    for /R D:\ %%i in (*.exe) do (
        echo %%i
        copy %%i H:\main
    )
) else (
    echo "H:\main already exists"
)
echo ON
