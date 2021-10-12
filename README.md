# Simple Raycast System

Простая система рейкастов для Unity3d. Частично поддерживает Enity Event System. Полезно для VR проектов с большими визуализациями данных, где не нужна физика коллизий, но нужно уметь что-то выбирать по лучу с помощью котроллера.

Установка через Unity Package Manager / Add package from git URL:
https://github.com/Nox7atra/SimpleRaycastSystem.git?path=/Assets/Package

**SimpleRaycaster** - аналог PhysicsRaycaster из Unity Event System. Работает с системой событий, если повесить его на камеру то события из интерфейсов IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler и т.п. отработают корректно.

**SimpleTransformRaycaster** - рейкастер пускающий луч от конкретного трансформа. Нужен в первую очередь для того чтобы использовать интерфейсы EventSystem в повесив данный компонент на VR контроллер. Известные проблемы - необходим кастомный Input Module для того, чтобы правильно отрабатывали события Drag, Up, Down, Click. На данный момент срабатывания идут от мыши, нужно расширить класс Standalone Input Module.

На данный момент поддерживается:

**Сферический коллайдер** - полностью
**Кубический коллайдер** - частично (не поддерживаются наклоны в некоторых плоскосях из-за упрощённого быстрого алгоритма коллизии, но для интерфейсов достаточно)

*SimpleRaycastSystem.Raycast  (Ray ray, out SimpleRaycastHit hit)* - метод для того, чтобы послать рейкаст в ближайший объект в ручную
*RaycastAll (Ray ray, out IEnumerable<SimpleRaycastHit> hits)* - метод для того, чтобы послать рейкасть во все объекты в ручную

На данный момент не поддерживаются - Layer Mask
