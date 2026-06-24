FROM php:8.2-fpm

# Instalar extensiones indispensables de MySQL para PHP-FPM
RUN docker-php-ext-install mysqli pdo pdo_mysql \
    && docker-php-ext-enable mysqli pdo_mysql

# Sincronizar zona horaria de desarrollo
RUN echo "date.timezone = 'America/Argentina/Buenos_Aires'" > /usr/local/etc/php/conf.d/timezone.ini