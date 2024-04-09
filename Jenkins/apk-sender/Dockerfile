# Используйте официальный образ Python в качестве основы
FROM python:3.9-slim

# Установите переменную окружения PYTHONUNBUFFERED=1 для предотвращения буферизации вывода
ENV PYTHONUNBUFFERED=1

# Установите рабочую директорию внутри контейнера
WORKDIR /app

# Скопируйте файл requirements.txt в контейнер
COPY requirements.txt .

# Установите зависимости из requirements.txt
RUN pip install -r requirements.txt

# Копируйте все файлы вашего проекта в контейнер
COPY . .

