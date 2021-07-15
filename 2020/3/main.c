#include <stdio.h>

struct SLOPE {
    unsigned int x;
    unsigned int y;
};

int main() {
    printf("Hello, World!\n");

    unsigned long total = 1;
    int i;
    struct SLOPE slopes[] = {{1, 1}, {3, 1}, {5, 1}, {7, 1}, {1, 2}};
    for (i = 0; i < 5; ++i) {
        FILE *fp = fopen("/home/sam/Documents/adventofcode/2020/3/input", "r");
        char c;
        unsigned int x = 0, y = 0, targetX = 0, targetY = 0;
        unsigned int trees = 0;

        while ((c = fgetc(fp)) != EOF) {
            if (c != '\n') {
                if (x == targetX && y == targetY) {
                    if (c == '#') trees++;
                    targetX += slopes[i].x;
                    targetY += slopes[i].y;
                }
                x++;
            } else {
                if (targetX >= x) targetX -= x;
                x = 0;
                y++;
            }
        }
        printf("trees: %d\n",trees);
        total *= trees;
    }

    printf("total: %lu\n",total);

    return 0;
}
