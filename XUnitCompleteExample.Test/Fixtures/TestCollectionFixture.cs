// Questa classe serve solo per associare alla stringa "TestCollectionFixture" una fixture cioè TestFixture.
// In altre parole con la classe TestCollectionFixture sto creando un tag (la stringa "TestCollectionFixture") che posso associare a qualsiasi classe contenente unit tests. Nella classe a cui ho
// assegnato il "tag" posso creare una istanza di TestFixture esattamente come dovrei fare con la dependency injection (private readonly e poi l'assegnazione del valore con il costruttore).
namespace XUnitCompleteExample.Fixtures;

[CollectionDefinition("TestCollectionFixture")]
public class TestCollectionFixture : ICollectionFixture<TestFixture>
{
}