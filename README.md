# Dependency Injection Container

Контейнер позволяет регистрировать зависимости в формате: Тип интерфейса (TDependency) -> Тип реализации (TImplementation), где TDependency — любой ссылочный тип данных, а TImplementation — не абстрактный класс, совместимый с TDependency, объект которого может быть создан.    
Контейнер отделен от своей конфигурации: сначала выполняется создание конфигурации и регистрация в нее зависимостей, а затем создание на ее основе контейнера. В момент создания контейнера обеспечивается валидация конфигурации.    
Внедрение зависимостей осуществляется через конструктор. Создание зависимостей выполняется рекурсивно. Предусмотрен способ получения сразу всех реализаций, если у одной зависимости их несколько.

Реализовано два варианта времени жизни зависимостей: 
- instance per dependency — каждый новый запрос зависимости из контейнера приводит к созданию нового объекта (по умолчанию);
- singleton — на все запросы зависимостей возвращается один экземпляр объекта.
