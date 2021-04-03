; ModuleID = 'Test'
source_filename = "Test"

@0 = constant [9 x i8] c"FizzBuzz\00"
@1 = constant [5 x i8] c"Fizz\00"
@2 = constant [5 x i8] c"Buzz\00"
@3 = constant [4 x i8] c"%i\0A\00"

define i32 @main() {
  %i = alloca i32, align 4
  store i32 0, i32* %i, align 4
  br label %1

1:                                                ; preds = %13, %0
  %2 = load i32, i32* %i, align 4
  %.not = icmp eq i32 %2, 100
  br i1 %.not, label %7, label %3

3:                                                ; preds = %1
  %4 = add i32 %2, 1
  store i32 %4, i32* %i, align 4
  %5 = srem i32 %4, 15
  %6 = icmp eq i32 %5, 0
  br i1 %6, label %8, label %10

7:                                                ; preds = %1
  ret i32 0

8:                                                ; preds = %3
  %9 = tail call i32 @puts(i8* nonnull dereferenceable(1) getelementptr inbounds ([9 x i8], [9 x i8]* @0, i64 0, i64 0))
  br label %13

10:                                               ; preds = %3
  %11 = srem i32 %4, 3
  %12 = icmp eq i32 %11, 0
  br i1 %12, label %14, label %16

13:                                               ; preds = %19, %21, %14, %8
  br label %1

14:                                               ; preds = %10
  %15 = tail call i32 @puts(i8* nonnull dereferenceable(1) getelementptr inbounds ([5 x i8], [5 x i8]* @1, i64 0, i64 0))
  br label %13

16:                                               ; preds = %10
  %17 = srem i32 %4, 5
  %18 = icmp eq i32 %17, 0
  br i1 %18, label %19, label %21

19:                                               ; preds = %16
  %20 = tail call i32 @puts(i8* nonnull dereferenceable(1) getelementptr inbounds ([5 x i8], [5 x i8]* @2, i64 0, i64 0))
  br label %13

21:                                               ; preds = %16
  %22 = tail call i32 (i8*, ...) @printf(i8* nonnull dereferenceable(1) getelementptr inbounds ([4 x i8], [4 x i8]* @3, i64 0, i64 0), i32 %4)
  br label %13
}

declare i32 @puts(i8*)

declare i32 @printf(i8*, ...)
