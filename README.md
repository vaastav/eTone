# eTone
Tone accuracy game

## Active links in game

If the game is deployed at 127.0.0.1:4000, then as an example, the login page will be at 127.0.0.1:4000/login.

+ admin : The link to the admin page. Successful login requires setting up a super user.
+ login : The link to the login page. Logins are only successful for previously registered users.
+ logout : The link used by django to handle logouts.
+ signup : The registration page.
+ game : The page where you can play the game.
+ stats : The stats page where you view the overall stats.
+ api/upload/<typeID>/<filename>/ : An experimental POST RESTful API. The typeID refers to the ID of the sound which was played for the user and the filename is the name of the uploaded recorded file. The file upload here is not fully functional and requires debugging. Although this doesn't really affect the game at all. Use the game link instead to play the game as that is fully functional.

## Running the game

```
    cd backend/server
    python3 manage.py runserver [IP:Port]
```

The game will be launched at IP:Port.

## Requirements

Here is the list of libraries needed to run eTone. Most of these are included as part of the Anaconda installation.
The game is designed to work with Python 3.5

+ django
+ scipy
+ numpy
+ rest_framework