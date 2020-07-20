# Parallel Programming Lab - Assignment 2
This is the second assignment, the topic is about parallel execution using **MPI.net**.

The project was created using **Visual Studio 2019**, but it should work fine on **Visual Studio 2013**, and was tested using **MPI.net v1.2.0** on **.Net Framework v4.0**.

The file `text-generator.php` at `PLP Assignment/bin/Debug` is used to generate a random text that consist of letters `A-Z`, `a-z` and digits `0-9`, here's how to use the file:

```
php text-generator.php NUMBER_OF_LINES MIN_CHARACTERS_PER_LINE MAX_CHARACTERS_PER_LINE
```

For example, to generate a text that contains 30 lines where each line have between 50 and 90 characters use the following command:

```
php text-generator.php 30 50 90
```

This generates something like:

```
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
```

The PHP script doesn't use system calls to write the output into a file, but you can redirect the output to a text file as following:

```
php text-generator.php 30 50 90 > input.txt
```

**Note**: the program expects the existence of a file called **input.txt** in the same directory as executable, it will be passed automatically to the program.

To execute the program, execute the following command:

```
mpiexec -n NUMBER_OF_PROCESSES "PLP Assignment.exe"
```

If you want detailed output, use the *--verbose* flag:

```
mpiexec -n NUMBER_OF_PROCESSES "PLP Assignment.exe" --verbose
```