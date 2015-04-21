


Public Enum enumWriteFileIf

    ALWAYS = 0              ' always generate file
    IF_NOT_EXISTS = 1       ' write if file does not exist only
    IF_CODE_CHANGED = 2     ' write if code is different from the existing code
    NEVER = 10              ' never write file , same as disabling a file type

End Enum
