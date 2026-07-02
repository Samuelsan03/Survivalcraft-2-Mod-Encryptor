# Survivalcraft 2 Mod Encryptor

## 📖 Descripción

**Survivalcraft 2 Mod Encryptor** es un mini-mod que restaura la funcionalidad de **Reforzamiento (Reinforcing)**. Esta característica permite encriptar archivos `.scmod` para añadir una capa extra de seguridad a tus mods. Al utilizarla, tu archivo quedará protegido y ya no podrá descomprimirse como un ZIP convencional, ayudando a prevenir el robo o la extracción no autorizada de tus recursos.

Esta útil función estuvo disponible en la versión **2.3** de Survivalcraft, pero fue eliminada sin motivo aparente en la versión **2.4** (con las APIs 1.8 y 1.9). Este mini-mod te permite recuperarla, para que puedas proteger tus proyectos sin tener que depender de una versión antigua del juego solo para encriptar.

**Importante**: A diferencia del sistema antiguo que guardaba los archivos en la carpeta `ModsCache`, esta versión mejorada crea una carpeta **independiente** llamada `EncryptedMods`. Esto mantiene tus mods originales y los encriptados perfectamente separados y organizados.

### ✨ Características principales

*   **Encriptación con un clic**: Protege cualquier mod instalado directamente desde el menú de ajustes del juego.
*   **Carpeta independiente**: Los archivos encriptados se guardan en `EncryptedMods`, separados de los mods originales y del caché.
*   **Compatibilidad total con dispositivos móviles**: Se ha corregido el error de acceso a la carpeta en Android, ahora funciona sin problemas en celulares.
*   **Interfaz sencilla**: Diálogos intuitivos que te guían en todo el proceso.
*   **Protección contra duplicados**: Nombra los archivos automáticamente para evitar sobrescribir otros.
*   **Detección inteligente**: Reconoce si un mod ya está encriptado para evitar procesos innecesarios.
*   **Manejo de errores**: Mensajes claros para ayudarte en caso de cualquier problema.

---

## 📋 Guía de Uso

### Paso 1: Acceder al Menú de Configuración  
<img width="1912" height="985" alt="image" src="https://github.com/user-attachments/assets/cdb2f24b-eb72-40cd-bc39-6918dcc0f7e9" />

En la pantalla principal del juego, selecciona **"CONFIGURACION"** para entrar a los ajustes.

### Paso 2: Encontrar el Botón de Encriptar  
<img width="1917" height="972" alt="image" src="https://github.com/user-attachments/assets/d28ea100-ef2a-47cd-a57d-8d2a8f9d1ebc" />

Dentro del menú de configuración, busca la opción **"ENCRIPTAR MOD"** y haz clic en ella.

### Paso 3: Seleccionar el Mod a Encriptar  
<img width="1917" height="927" alt="image" src="https://github.com/user-attachments/assets/e076abb5-32a5-4d34-9846-9adaf5b1076b" />

Aparecerá una lista con todos los mods que tienes instalados. Selecciona el que deseas proteger.

### Paso 4: Confirmación y Encriptación  
<img width="1917" height="967" alt="image" src="https://github.com/user-attachments/assets/2c76901b-2e6b-45e6-8352-63e814037ee9" />  
<img width="1352" height="757" alt="image" src="https://github.com/user-attachments/assets/02abf2e8-b14c-49b8-a543-9e390d896da6" />

Al terminar, verás un mensaje de confirmación: **"EL MOD SE HA ENCRIPTADO CORRECTAMENTE."**  
El archivo encriptado se guardará en la carpeta `EncryptedMods`, dentro del directorio del juego.

### Paso 5: Ubicar el Archivo Encriptado  
<img width="792" height="757" alt="image" src="https://github.com/user-attachments/assets/42958f5c-5da8-42f8-83b4-66f11d0d20c0" />  
<img width="788" height="761" alt="image" src="https://github.com/user-attachments/assets/c1130fc2-9055-43d9-9e47-301d7caafcb1" />

Puedes encontrar tu mod encriptado en la ruta:
```
📁 EncryptedMods
  └── [2.4]Cloud Backpack (Encrypted).scmod
```

**Nota sobre el sistema de almacenamiento**:
- **Versión 2.3 (antigua)**: Guardaba los archivos en `ModsCache`.
- **Versión 2.4 con este mod (nueva)**: Guarda los archivos en `EncryptedMods` (carpeta independiente).

Esto significa que tus mods originales permanecen intactos en la carpeta `Mods`, los archivos encriptados se guardan por separado y no interfieren con el caché del juego.

### ⚠️ Nota Importante sobre el Archivo  
<img width="1107" height="757" alt="image" src="https://github.com/user-attachments/assets/14f915d2-fa8a-44bb-a796-abac168b0b60" />

**¡No te alarmes si ves este mensaje!**  
Al intentar abrir el archivo `.scmod` encriptado directamente, podrías ver:
> "El archivo tiene un formato desconocido o está dañado"

**Esto no es un error**, sino el funcionamiento correcto de la encriptación. El archivo está protegido y su estructura ha sido ofuscada a propósito, por lo que los visores estándar no podrán leerlo. Sin embargo, sigue siendo completamente funcional para los cargadores de mods compatibles.

---

## 💡 Ventajas de Usar este Encriptador

*   **Protege tu trabajo**: Evita que otros modifiquen o roben los recursos de tu mod sin permiso.
*   **Mantiene la integridad**: Asegura que tu mod funcione como tú lo diseñaste.
*   **Fácil de usar**: No necesitas conocimientos técnicos para proteger tus archivos.
*   **Seguro**: Los archivos originales no se modifican, solo se crea una copia encriptada.
*   **Organización**: La carpeta `EncryptedMods` mantiene tus archivos protegidos separados y ordenados.
*   **Comodidad**: Recupera una función perdida sin tener que cambiar de versión del juego.
*   **Sin conflictos**: Al no usar `ModsCache`, evitas posibles conflictos con otros sistemas del juego.
*   **Totalmente funcional en Android**: Se ha solucionado el error de permisos que impedía crear la carpeta en dispositivos móviles.

---

## 🔧 Corrección de errores en dispositivos móviles

En versiones anteriores, al intentar encriptar en Android se producía un error de acceso:
```
ERROR: Access denied to "app:/EncryptedMods".
```
Esto ocurría porque la ruta `app:/EncryptedMods` no era reconocida correctamente en el sistema de archivos del juego.

**Esta versión soluciona el problema** utilizando la ruta correcta mediante `ModsManager.ExternalPath + "/EncryptedMods"`, lo que garantiza que la carpeta se cree en la ubicación adecuada y con los permisos necesarios. Ahora, los jugadores de dispositivos móviles pueden encriptar sus mods sin ningún inconveniente.

---

## ❓ Solución de Problemas

*   **"Archivo no encontrado"**: Asegúrate de que el mod seleccionado exista en la carpeta `Mods`.
*   **"Extensión inválida"**: Solo se pueden encriptar archivos con extensión `.scmod`.
*   **"Ya está encriptado"**: El mod ya ha sido protegido anteriormente.
*   **El archivo no aparece en la carpeta**: Verifica los permisos de escritura en la carpeta del juego y que la carpeta `EncryptedMods` se haya creado correctamente.
*   **Error de formato al abrir**: Es normal, el archivo está encriptado y no se puede abrir con herramientas estándar.
*   **Problemas en Android**: Si aún tienes errores, asegúrate de que el juego tenga permisos de almacenamiento y que estés usando la versión más reciente del mod.

---

## 👨‍💻 Acerca del Desarrollador

*   **Autor**: Samuelsan03
*   **Lenguaje**: C# 100%
*   **Repositorio**: [GitHub - Survivalcraft-2-Mod-Encryptor](https://github.com/Samuelsan03/Survivalcraft-2-Mod-Encryptor)

---

## 📜 Nota Final

Este mod está diseñado para proteger tu trabajo y el de otros creadores. Úsalo de manera ética y siempre respeta los derechos de autor de los mods que no sean tuyos. ¡Feliz creación!
