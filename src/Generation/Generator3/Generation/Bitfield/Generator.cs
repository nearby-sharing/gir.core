﻿namespace Generator3.Generation.Bitfield
{
    public class Generator
    {
        private readonly Template<Model> _template;
        private readonly Publisher _publisher;

        public Generator(Template<Model> template, Publisher publisher)
        {
            _template = template;
            _publisher = publisher;
        }

        public void Generate(GirModel.Bitfield bitfield)
        {
            var model = new Model(bitfield);
            var source = _template.Render(model);
            var codeUnit = new CodeUnit(bitfield.Namespace.GetCanonicalName(), bitfield.Name, source);
            _publisher.Publish(codeUnit);
        }
    }
}
