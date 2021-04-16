Не важно на какой технологии реализован сайт HTML&JS, JQuery, React, Angular. Когда пользователь открывает сайт - он не знает как он реализован, и это не имеет значения. Важно чтобы пользователь мог найти нужные элементы и повзаимодействоать с ними. Точно так же и с автоматизацией в браузере. Нужно только правильно описать селекторы чтобы найти элементы в HTML и повзаимодействоать с ними. От технологии может зависеть способ пробрасывания дополнительных атрибутов в HTML для более удобного поиска элементов

- Пример интеграции с NUnit (SetUp TearDown)
- про модульное тестированвие контролов
- про то как инициализировать браузер
- про переимпользуемость браузера
- про многопоточные запуски
- Особенности тестирования: размер экрана, отключение анимации, фиксиорвание версии драйвера и браузера и т.д. можно подсмотреть в эврика-вики
- про селекторы тоже отдельная статья
- есть две отдельные истории:
  1. про то как искать  элементы в в верстке
  1. про то как в верстку накинуть атрибутов для удобного поиска

- про возможность отключения анимаций на странице для повышения стабильности и для ускорения
- про отлов ошибок в консоли браузера - если возникла ошибка то, то ее стоит залогировать, сделать сценарий красным и может иметь смысл прервать сценарий дострочно


```
public class Portal : ControlBase
{
    public Portal(IContextBy contextBy)
        : this(contextBy.SearchContext, contextBy.By)
    {
    }

    public Portal(ISearchContext context, By selector)
        : base(context, new PortalBy(selector))
    {
    }

    private class PortalBy : By
    {
        private readonly By selector;

        public PortalBy(By selector)
        {
            this.selector = selector;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var id = context.FindElement(selector).GetAttribute("data-render-container-id");
            return context.Root().FindElement(By.CssSelector($"[data-rendered-container-id='{id}']"));
        }
    }
}
```


```
public class FormInput : Input
{
    public FormInput(IContextBy contextBy) : base(contextBy.SearchContext, new FormInputBy(contextBy.By))
    {
    }

    private class FormInputBy : By
    {
        private readonly By selector;

        public FormInputBy(By selector)
        {
            this.selector = selector;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            return context.FindElement(selector).FindElement(By.CssSelector("label"));
        }
    }
}
```

# Что хочется видеть в идеальной доке
## Что такое Селон
- Что он делает и умеет. Какие потребности покрывает.
- Что он не делает. Что ещё надо будет настроить, скачать.
- Состав Селона: ожидания, поиск, работа со списками и прочее. 

## Как начать
- Описание из чего состоит минимальный проект с тестами. Браузер, драйвер, селон, компоненты и тп. 
- Что и где надо будет скачать. Со ссылками.

## Описание АПИ библиотеки
- Какие есть методы, что они делают, примеры кода. [Пример](https://webdriver.io/docs/api/element/getSize), который мне нравится.

## Примеры кода
- Тестовый проект с базовой обвязкой. Чтобы оно в целом работало и показывало как может выглядеть проект.
- Сложные, нетипичные, редкие ситуации и их решение. Н-р, страница автоматически прокручивается после загрузки.