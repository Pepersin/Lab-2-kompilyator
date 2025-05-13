2. Разработка грамматики
Определим грамматику структур на G[‹Def›] для enum в PHP:

Продукции P:
1) ⟨Def⟩ → "enum " ⟨Enum⟩
2) ⟨Enum⟩ → ⟨Name⟩ ' ' '{' ⟨Body⟩
3) ⟨Body⟩ → 'case ' ⟨Name⟩ ';' ⟨CaseRem⟩
4) ⟨CaseRem⟩ → 'case ' ⟨Name⟩ ';' ⟨CaseRem⟩
5) ⟨CaseRem⟩ → '}' ⟨End⟩
6) ⟨End⟩ → ';'
Разбор имени
7) ⟨Name⟩ → ⟨Letter⟩ ⟨NameRem⟩
8) ⟨NameRem⟩ → ⟨Letter⟩ ⟨NameRem⟩
9) ⟨NameRem⟩ → ⟨Digit⟩ ⟨NameRem⟩
10) ⟨NameRem⟩ → ε
Алфавит:
11) ⟨Letter⟩ → "a" | "b" | ... | "z" | "A" | ... | "Z"
12) ⟨Digit⟩ → "0" | "1" | ... | "9"

Следуя введенному формальному определению грамматики, представим G[‹Def›] ее составляющими:
Z = ‹Def› — начальный символ
VT = {a, b, ..., z, A, ..., Z, 0, ..., 9, _, {, }, ;, ., :, ' ', '\n'} — множество терминальных символов
VN = {‹Def›, ‹Enum›, ‹Name›, ‹NameRem›, ‹Body›, ‹Case›, ‹CaseRem›, ‹End›, ‹Letter›, ‹Digit›} — множество нетерминальных символов
3. Классификация грамматики
Согласно классификации Хомского, грамматика G[‹Def›] относится ко второму классу иерархии Хомского (Контекстно-свободная грамматика). Все правила (1)-(12) относятся к контекстно-свободной грамматике (A → α), где 
А- один нетерминал, а α- произвольная строка из терминалов и/или нетерминалов.
4. Метод анализа
Грамматика G[‹Def›] является контекстно-свободной грамматикой.
На рисунке 1 представлена диаграмма состояний сканера.
![image](https://github.com/user-attachments/assets/a5f569c1-3357-476c-9bfa-9c3e9cee0ccf)
Правила (1) – (12) для G[‹Def›] реализованы на графе (рисунок 2).
Сплошные стрелки на графе характеризуют синтаксически верный разбор; пунктирные символизируют состояние ошибки (ERROR).
Состояние END символизирует успешное завершение разбора.
![image](https://github.com/user-attachments/assets/8bd51280-b6b0-4e12-b3b6-dbfa0fe1a901)
На рисунках 3-8 представлены тестовые примеры запуска разработанного лексического анализатора объявления перечисления на языке PHP
![image](https://github.com/user-attachments/assets/a71d177a-742a-4c25-9df0-30b0b354695f)
Рис.1
![image](https://github.com/user-attachments/assets/c9db92b8-8d2a-4756-b4ae-1cb15a8c6adf)
Рис.2
![image](https://github.com/user-attachments/assets/1bacae84-f8e5-4e4a-91be-b3a8099cabc3)
Рис.3
![image](https://github.com/user-attachments/assets/07fe64b3-1787-4c8c-9faf-e2fa930739cf)
Рис.4
![image](https://github.com/user-attachments/assets/de4a7740-f42b-453f-8cf9-ec71e411010b)
Рис.5
![image](https://github.com/user-attachments/assets/447e094f-6d5c-48f5-ac10-ef84adebed47)
Рис.6







