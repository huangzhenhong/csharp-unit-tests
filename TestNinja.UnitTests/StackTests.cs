using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    [Category("StackTests")]
    public class StackTests
    {
        private Stack<string> _stack;

        [SetUp]
        public void SetUp()
        {
            _stack = new Stack<string>();
        }

        [Test]
        public void Push_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // var _stack = new Stack<object>();
            Assert.That(() => _stack.Push(null), Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Push_ValidArgument_ReturnCountOne() {
            _stack.Push("a");
            Assert.That(_stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Pop_EmptyStack_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stack.Pop(), Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Pop_StackWithAFewObjects_ReturnObjectOnTheTop()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");
            Assert.That(() => _stack.Pop(), Is.EqualTo("c"));
        }

        [Test]
        public void Pop_StackWithAFewObjects_RemovesObjectOnTheTop()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");
            _stack.Pop();

            Assert.That(_stack.Count , Is.EqualTo(2));
        }

        [Test]
        public void Peek_EmptyStack_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stack.Peek(), Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Peek_StackWithObjects_ReturnTheObjectOnTop()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");
            Assert.That(() => _stack.Peek(), Is.EqualTo("c"));
        }

        [Test]
        public void Peek_StackWithObjects_DoesNotRemoveTheObject()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");
            _stack.Peek();
            Assert.That(_stack.Count, Is.EqualTo(3));
        }
    }
}
