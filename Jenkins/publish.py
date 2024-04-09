mport os
import requests

def send_apk_to_telegram(bot_token, chat_id, apk_path):
    url = f'https://api.telegram.org/bot{bot_token}/sendDocument'
    files = {'document': open(apk_path, 'rb')}
    data = {'chat_id': chat_id}
    response = requests.post(url, files=files, data=data)
    if response.status_code == 200:
        print("APK successfully sent to Telegram!")
    else:
        print("Failed to send APK to Telegram. Status code:", response.status_code)

# Укажите ваш токен бота и chat_id получателя
bot_token = '6793739177:AAFRJZH4JvBaxzHV-nYsGT33lxw_7mDcZaI'
chat_id = '-1002065699771'

# Получение пути к текущей директории
current_dir = os.path.dirname(os.path.realpath(__file__))

# Путь к APK файлу
apk_path = os.path.join(current_dir, 'akvt_raspisanie/build/app/outputs/flutter-apk/app-release.apk')

send_apk_to_telegram(bot_token, chat_id, apk_path)