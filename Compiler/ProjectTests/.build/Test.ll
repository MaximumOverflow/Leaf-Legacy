; ModuleID = 'Test'
source_filename = "Test"

@singleton_attrib__VarArg = global {} zeroinitializer
@singleton_attrib__External = global {} zeroinitializer

declare i32 @printf(i8*, ...)

define i32 @main() {
  %a = alloca i32, align 4
  store i32 42, i32* %a, align 4
  store i32 69, i32* %a, align 4
  %1 = load i32, i32* %a, align 4
  ret i32 %1
}
