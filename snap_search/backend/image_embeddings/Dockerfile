FROM python:3.8
WORKDIR /app
RUN pip install pipenv
COPY Pipfile Pipfile.lock /app/
RUN pipenv lock -r > requirments.txt
RUN pip install -r requirments.txt
COPY . /app
CMD [ "python", "server.py" ]
EXPOSE 5000

