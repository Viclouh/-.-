import os
import requests
import sys

def send_apk_to_telegram(bot_token, chat_id, apk_path):
    url = f'https://api.telegram.org/bot{bot_token}/sendDocument'
    files = {'document': open(apk_path, 'rb')}
    data = {'chat_id': chat_id}
    response = requests.post(url, files=files, data=data)
    if response.status_code == 200:
        print("APK successfully sent to Telegram!")
    else:
        print("Failed to send APK to Telegram. Status code:", response.status_code)

if __name__ == "__main__":
    # Укажите ваш токен бота и chat_id получателя
    bot_token = '6793739177:AAFRJZH4JvBaxzHV-nYsGT33lxw_7mDcZaI'
    chat_id = '-1002065699771'

    # Проверяем, был ли передан путь к APK в качестве аргумента командной строки
    if len(sys.argv) < 2:
        print("Usage: python script.py <apk_path>")
        sys.exit(1)
    
    # Получаем путь к APK из аргумента командной строки
    apk_path = sys.argv[1]

    # Проверяем, существует ли указанный файл APK
    if not os.path.exists(apk_path):
        print("APK file not found:", apk_path)
        sys.exit(1)

    send_apk_to_telegram(bot_token, chat_id, apk_path)