FROM microsoft/aspnet:1.0.0-beta6

RUN apt-get -qq update
RUN apt-get -qqy install git python

WORKDIR tmp

RUN curl -O http://nodejs.org/dist/v0.10.35/node-v0.10.35.tar.gz
RUN tar xvfz node-v0.10.35.tar.gz

WORKDIR node-v0.10.35

RUN ./configure --prefix=/usr
RUN make
RUN make install
RUN npm install -g grunt-cli

WORKDIR ../..
RUN rm -rf tmp

COPY . /app
WORKDIR /app
RUN ["dnu", "restore"]

EXPOSE 5004
ENTRYPOINT ["dnx", "-p", "project.json", "kestrel"]
