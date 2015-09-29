# GoldMine
Hosting api server needs http namespace configuration in windows
please do the following:
netsh http add urlacl url=http://+:{port in app.config}/ user={username}
