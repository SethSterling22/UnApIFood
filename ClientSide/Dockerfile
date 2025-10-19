# Paso 6: Construir la aplicación Angular
FROM node:18 as angular
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Paso 7: Crear una imagen con el servidor web
FROM httpd:alpine
WORKDIR /usr/local/apache2/htdocs

# Paso 8: Copiar los archivos de la aplicación construida
COPY --from=angular ../UnApIFood/ .

# Paso 9: Configurar el servidor para redireccionar rutas a index.html
COPY httpd.conf /usr/local/apache2/conf/httpd.conf