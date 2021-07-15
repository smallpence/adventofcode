import java.io.File

fun main(args: Array<String>) {
    val preambleLength = 25

    "90655775622556".toLong()

    val nums = File("input")
        .readLines()
        .filter { line -> line.isNotEmpty() }
        .map { line -> line.toLong() }

    var i = 0;
    nums.forEach { num ->
        if (i >= preambleLength) {
            println("try $num")
            val previousNums = nums.subList(i - preambleLength, i)
            val result = previousNums.any { previousNum1 ->
                previousNums.any { previousNum2 ->
                    previousNum1 + previousNum2 == num
                }
            }
            println("${num} -> ${result}")
            if (!result) return
        }
        i++
    }
    println("hello world_")
}