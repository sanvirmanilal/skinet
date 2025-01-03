var todos = [];
function addTodo(title) {
    var newTodo = {
        id: todos.length + 1,
        title: title,
        completed: false
    };
    todos.push(newTodo);
    return newTodo;
}
function toggleTodo(id) {
    var todo = todos.find(function (todo) { return todo.id === id; });
    if (todo) {
        todo.completed = !(todo === null || todo === void 0 ? void 0 : todo.completed);
    }
}
addTodo("Build Api");
addTodo("Publish Api");
toggleTodo(1);
console.log(todos);
