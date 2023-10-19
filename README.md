# APIRestTT

Utilice el patron de intersección para hacer la encriptación de la contraseña. Este utiliza una codificación Base64.

Las medidas de seguridad que se pueden implementar en este tipo de API son:
  -Rate limiting, el cual se implemento para no permitir hacer spam a las llamadas.
  -Autenticación y autorización que este viene implicito a la hora de crear una API para Inicio de sesión así que esta no se toma en cuenta como medida seleccionada de seguridad.
  -Las conexiónes disponibles por usuario, esta se implemento teniendo solamente un acceso por usuario.
  -Tambien podemos implementar la auditoria, con esto revisamos que es lo que está siendo actualizado por los usuario.
  -Restricción a diferentes accesos a la información, dependiendo del tipo de usuario se delimitara la interacción con información sensible.
  -Se podrá implementar alertas y monitores de anomalias.
  -Así como gateways con lo que podrémos filtrar cualquier intento sospechoso de conexión a la API.
  -Igualmente la encriptación de los datos.
  -Una WAF para protección contra DDoS.
