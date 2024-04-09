
class LevenshteinDistance{
  static int  levenshteinDistance(String s1, String s2) {
    int m = s1.length;
    int n = s2.length;

    // Создаем матрицу для хранения расстояний
    List<List<int>> matrix = List.generate(m + 1, (_) => List<int>.filled(n + 1, 0));

    // Инициализация первой строки и столбца
    for (int i = 0; i <= m; i++) {
      matrix[i][0] = i;
    }
    for (int j = 0; j <= n; j++) {
      matrix[0][j] = j;
    }

    // Заполнение матрицы
    for (int i = 1; i <= m; i++) {
      for (int j = 1; j <= n; j++) {
        int cost = s1[i - 1] == s2[j - 1] ? 0 : 1;
        matrix[i][j] = (matrix[i - 1][j] + 1).compareTo(matrix[i][j - 1] + 1) > 0
            ? (matrix[i][j - 1] + 1).compareTo(matrix[i - 1][j - 1] + cost) > 0
            ? matrix[i - 1][j - 1] + cost
            : matrix[i][j - 1] + 1
            : (matrix[i - 1][j] + 1).compareTo(matrix[i - 1][j - 1] + cost) > 0
            ? matrix[i - 1][j - 1] + cost
            : matrix[i - 1][j] + 1;
      }
    }

    // Возвращаем значение расстояния
    return matrix[m][n];
  }
}
