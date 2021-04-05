; ModuleID = 'Test'
source_filename = "Test"

%struct_Test_test = type { i32 }

@0 = constant [9 x i8] c"FizzBuzz\00"
@1 = constant [5 x i8] c"Fizz\00"
@2 = constant [5 x i8] c"Buzz\00"
@3 = constant [4 x i8] c"%i\0A\00"

define void @function_Test_fizzBuzz_i32_void(i32 %0) {
  %2 = alloca i32, align 4
  store i32 %0, i32* %2, align 4
  %i = alloca i32, align 4
  store i32 0, i32* %i, align 4
  br label %3

3:                                                ; preds = %15, %1
  %4 = load i32, i32* %i, align 4
  %.not = icmp eq i32 %4, %0
  br i1 %.not, label %9, label %5

5:                                                ; preds = %3
  %6 = add i32 %4, 1
  store i32 %6, i32* %i, align 4
  %7 = srem i32 %6, 15
  %8 = icmp eq i32 %7, 0
  br i1 %8, label %10, label %12

9:                                                ; preds = %3
  ret void

10:                                               ; preds = %5
  %11 = tail call i32 @puts(i8* nonnull dereferenceable(1) getelementptr inbounds ([9 x i8], [9 x i8]* @0, i64 0, i64 0))
  br label %15

12:                                               ; preds = %5
  %13 = srem i32 %6, 3
  %14 = icmp eq i32 %13, 0
  br i1 %14, label %16, label %18

15:                                               ; preds = %21, %23, %16, %10
  br label %3

16:                                               ; preds = %12
  %17 = tail call i32 @puts(i8* nonnull dereferenceable(1) getelementptr inbounds ([5 x i8], [5 x i8]* @1, i64 0, i64 0))
  br label %15

18:                                               ; preds = %12
  %19 = srem i32 %6, 5
  %20 = icmp eq i32 %19, 0
  br i1 %20, label %21, label %23

21:                                               ; preds = %18
  %22 = tail call i32 @puts(i8* nonnull dereferenceable(1) getelementptr inbounds ([5 x i8], [5 x i8]* @2, i64 0, i64 0))
  br label %15

23:                                               ; preds = %18
  %24 = tail call i32 (i8*, ...) @printf(i8* nonnull dereferenceable(1) getelementptr inbounds ([4 x i8], [4 x i8]* @3, i64 0, i64 0), i32 %6)
  br label %15
}

declare i32 @puts(i8*)

declare i32 @printf(i8*, ...)

define %struct_Test_test @function_Test___add_lref_struct_Test_test_struct_Test_test_struct_Test_test(%struct_Test_test* %0, %struct_Test_test %1) {
  %3 = extractvalue %struct_Test_test %1, 0
  %4 = getelementptr inbounds %struct_Test_test, %struct_Test_test* %0, i64 0, i32 0
  %5 = load i32, i32* %4, align 4
  %6 = add i32 %5, %3
  %7 = insertvalue %struct_Test_test undef, i32 %6, 0
  ret %struct_Test_test %7
}
